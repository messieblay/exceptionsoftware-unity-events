using ExceptionSoftware.ExEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExceptionSoftware.Events
{
    public static class ExEventsCodeGenerator
    {
        public struct Options
        {
            public string className { get; set; }
            public string classNameGetCode { get; set; }
            public string namespaceName { get; set; }
            public string sourceAssetPath { get; set; }
        }

        public static void GenerateLayer(Layer layer, string gamenamespace)
        {
            Options options = default;
            options.className = CSharpCodeHelpers.MakeTypeName(layer.name) + "EventLayer";
            options.classNameGetCode = layer.LastType != string.Empty ? layer.LastType : options.className;

            options.namespaceName = gamenamespace;


            //Recuperación de la clase anterior. De ahi obtendré el código viejo
            List<NestedClass> nestedClasses = null;
            List<System.Type> types = ExReflect.FindTypes(options.classNameGetCode);

            foreach (Type t in types)
            {
                if (t.IsSubclassOf(typeof(EventLayer)))
                {
                    nestedClasses = CSharpCodeHelpers.GetNestedClassCode(t);
                    break;
                }
            }


            //Edicion del template
            string newfile = GenerateWrapperCode(layer, nestedClasses, options);

            //Creacion de directorio
            Directory.CreateDirectory(ExEventsEditorUtility.EVENTS_SCRIPTS_PATH);

            //Reemplazar fichero
            string finalPath = ExEventsEditorUtility.EVENTS_SCRIPTS_PATH + "/" + options.className + ".cs";
            File.WriteAllText(finalPath, newfile);

            //Guardamos las referencias de los ultimos scripts creados para las sucesivas modificaciones
            layer.LastType = options.className;
            foreach (var e in layer.events)
            {
                e.LastName = e.name;
            }
        }

        public static string GenerateWrapperCode(Layer layer, List<NestedClass> oldNestedClasses, Options options)
        {
            if (layer == null)
                throw new ArgumentNullException(nameof(layer));

            var writer = new Writer
            {
                buffer = new StringBuilder()
            };

            // Header.
            if (!string.IsNullOrEmpty(options.sourceAssetPath))
                writer.WriteLine($"// GENERATED AUTOMATICALLY FROM '{options.sourceAssetPath}'\n");


            // Usings.
            writer.WriteLine("using ExceptionSoftware.Events;");
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("");

            // Begin namespace.
            {
                var haveNamespace = !string.IsNullOrEmpty(options.namespaceName);
                if (haveNamespace)
                {
                    writer.WriteLine($"namespace {options.namespaceName}");
                    writer.BeginBlock();
                }

                // Begin class.
                {
                    writer.WriteLine($"public class {options.className} : EventLayer  ");
                    writer.BeginBlock();

                    //Events
                    foreach (var v in layer.events)
                    {
                        writer.WriteLine($"//Events");
                        writer.WriteLine($"public Event<{CSharpCodeHelpers.MakeTypeName(v.name)}> {v.name.ToLower()};");
                    }

                    writer.WriteLine("");

                    //Evetn Models
                    string oldcode;
                    foreach (var v in layer.events)
                    {
                        oldcode = string.Empty;
                        if (oldNestedClasses != null)
                        {
                            NestedClass nested;
                            if (v.LastName != string.Empty)
                            {
                                nested = oldNestedClasses.Find(s => s.name.ToLower() == v.LastName.ToLower());
                            }
                            else
                            {
                                nested = oldNestedClasses.Find(s => s.name.ToLower() == v.name.ToLower());
                            }

                            if (nested != null)
                            {
                                oldcode = nested.code;
                            }
                        }

                        writer.WriteLine($"public class {CSharpCodeHelpers.MakeTypeName(v.name)} : EventModel");
                        writer.BeginBlock();

                        writer.WriteLine(oldcode);

                        writer.EndBlock();
                    }


                    writer.WriteLine("");




                    // End class.
                    writer.EndBlock();
                }

                // End namespace.
                if (haveNamespace)
                    writer.EndBlock();
            }
            return writer.buffer.ToString();

        }
    }
}
