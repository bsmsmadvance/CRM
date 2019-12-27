using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Database.UpdateDescApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting..");
            DbContextFactory factory = new DbContextFactory();
            var db = factory.CreateDbContext();


            List<Type> models = new List<Type>();
            foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (assembly.FullName.Contains("Database.Models"))
                {
                    models = assembly.GetTypes()
                                      .Where(t => t.Namespace != null && t.Namespace.Contains("Database.Models"))
                                      .ToList();
                    break;
                }
            }

            foreach (var model in models)
            {
                var tableAttr = model.GetCustomAttribute<TableAttribute>();
                var descAttr = model.GetCustomAttribute<DescriptionAttribute>();
                if (tableAttr != null && descAttr != null)
                {
                    //drop if existed
                    try
                    {
                        string dropScript = $@"
                            exec sp_dropextendedproperty  
                                 @name = N'MS_Description' 
                                ,@level0type = N'Schema', @level0name = '{tableAttr.Schema}' 
                                ,@level1type = N'Table',  @level1name = '{tableAttr.Name}' 
                            ";
                        db.Database.ExecuteSqlCommand(dropScript);
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        string addScript = $@"
                                exec sp_addextendedproperty  
                                     @name = N'MS_Description' 
                                    ,@value = N'{descAttr.Description}' 
                                    ,@level0type = N'Schema', @level0name = '{tableAttr.Schema}' 
                                    ,@level1type = N'Table',  @level1name = '{tableAttr.Name}' 
                                ";
                        db.Database.ExecuteSqlCommand(addScript);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Console.WriteLine($"Add table description of {tableAttr.Schema}.{tableAttr.Name}");
                }

                if (tableAttr != null)
                {
                    var properties = model.GetProperties().ToList();
                    foreach (var prop in properties)
                    {
                        var propDescAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                        if (propDescAttr != null)
                        {
                            //drop if existed
                            try
                            {
                                string dropScript = $@"
                            exec sp_dropextendedproperty  
                                 @name = N'MS_Description' 
                                ,@level0type = N'Schema', @level0name = '{tableAttr.Schema}' 
                                ,@level1type = N'Table',  @level1name = '{tableAttr.Name}' 
                                ,@level2type = N'Column', @level2name = '{prop.Name}'
                            ";
                                db.Database.ExecuteSqlCommand(dropScript);
                            }
                            catch (Exception ex)
                            {
                            }

                            try
                            {
                                string addScript = $@"
                                exec sp_addextendedproperty  
                                     @name = N'MS_Description' 
                                    ,@value = N'{propDescAttr.Description}' 
                                    ,@level0type = N'Schema', @level0name = '{tableAttr.Schema}' 
                                    ,@level1type = N'Table',  @level1name = '{tableAttr.Name}'
                                    ,@level2type = N'Column', @level2name = '{prop.Name}'
                                ";
                                db.Database.ExecuteSqlCommand(addScript);
                            }
                            catch (Exception ex)
                            {

                            }
                            Console.WriteLine($"Add column description of {tableAttr.Schema}.{tableAttr.Name}.{prop.Name}");
                        }
                    }
                }
            }

            Console.WriteLine("Finished.");
        }
    }
}
