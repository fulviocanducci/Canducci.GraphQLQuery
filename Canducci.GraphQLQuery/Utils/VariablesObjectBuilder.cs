﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using Canducci.GraphQLQuery.Interfaces;

namespace Canducci.GraphQLQuery.Utils
{
   internal class VariablesObjectBuilder: IDisposable
   {
      const MethodAttributes METHOD_ATTRIBUTES = 
         MethodAttributes.Public | 
         MethodAttributes.SpecialName | 
         MethodAttributes.HideBySig;

      private ModuleBuilder ModuleBuilder;
      private ConcurrentDictionary<string, Type> Types =
         new ConcurrentDictionary<string, Type>();

      public static VariablesObjectBuilder Create()
      {
         return new VariablesObjectBuilder();         
      }
      public VariablesObjectBuilder()
      {
         var assemblyBuilder = AssemblyBuilder
               .DefineDynamicAssembly(new AssemblyName("Canducci.GraphQL"), 
                  AssemblyBuilderAccess.RunAndCollect);
         ModuleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
      }

      public object CreateObjectWithValues(IList<IVariableValue> parameters)
      {
         object obj = null;
         if (parameters != null && parameters.Any())
         {
            var objType = CreateClass(parameters);
            obj = Activator.CreateInstance(objType);
            foreach (var prop in objType.GetProperties())
            {
               var result = parameters.Where(x => x.Name == prop.Name).FirstOrDefault();
               prop.SetValue(obj, result?.Value ?? null);
            }
         }
         return obj;
      }

      internal Type CreateClass(IList<IVariableValue> parameters, string className = "variables")
      {
         if (string.IsNullOrWhiteSpace(className) == false && Types.ContainsKey(className))
         {
            return Types[className];
         }

         var typeBuilder = ModuleBuilder.DefineType(className ?? Guid.NewGuid().ToString(), TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);
         typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
         foreach (var parameter in parameters)
            CreateProperty(typeBuilder, parameter.Name, parameter.Type);
         var type = typeBuilder.CreateTypeInfo().AsType();
         Types.TryAdd(type.FullName, type);
         return type;
      }

      private PropertyBuilder CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
      {
         var fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

         var propBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
         propBuilder.SetGetMethod(DefineGet(typeBuilder, fieldBuilder, propBuilder));
         propBuilder.SetSetMethod(DefineSet(typeBuilder, fieldBuilder, propBuilder));

         return propBuilder;
      }

      private MethodBuilder DefineSet(TypeBuilder typeBuilder, FieldBuilder fieldBuilder, PropertyBuilder propBuilder)
          => DefineMethod(typeBuilder, $"set_{propBuilder.Name}", null, new[] { propBuilder.PropertyType }, il =>
          {
             il.Emit(OpCodes.Ldarg_0);
             il.Emit(OpCodes.Ldarg_1);
             il.Emit(OpCodes.Stfld, fieldBuilder);
             il.Emit(OpCodes.Ret);
          });

      private MethodBuilder DefineGet(TypeBuilder typeBuilder, FieldBuilder fieldBuilder, PropertyBuilder propBuilder)
          => DefineMethod(typeBuilder, $"get_{propBuilder.Name}", propBuilder.PropertyType, Type.EmptyTypes, il =>
          {
             il.Emit(OpCodes.Ldarg_0);
             il.Emit(OpCodes.Ldfld, fieldBuilder);
             il.Emit(OpCodes.Ret);
          });

      private MethodBuilder DefineMethod(TypeBuilder typeBuilder, string methodName, Type propertyType, Type[] parameterTypes, Action<ILGenerator> bodyWriter)
      {
         var methodBuilder = typeBuilder.DefineMethod(methodName, METHOD_ATTRIBUTES, propertyType, parameterTypes);
         bodyWriter(methodBuilder.GetILGenerator());
         return methodBuilder;
      }
      public Type GetTypeFromName(string className) => Types.ContainsKey(className) ? Types[className] : null;

      public void Dispose()
      {
         ModuleBuilder = null;
         Types = null;
         GC.SuppressFinalize(this);
      }
   }
}
