using System;
using System.Linq;
using System.Text;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Installer;

namespace Session.Redis
{
  
        public class Bootstrapper
        {
            public IWindsorContainer container = new WindsorContainer();
            public IKernel kernel { get; private set; }

            public Bootstrapper(bool isHttpEnabled = false)
            {
                this.container.AddFacility<TypedFactoryFacility>();

               

                this.container.Install(FromAssembly.InDirectory(new AssemblyFilter( string.Empty)));

                this.kernel = this.container.Kernel;

               

                CheckCastleRegisterComponent();


            }

            protected void CheckCastleRegisterComponent()
            {
            

                var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
                var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

                var handlers = diagnostics.Inspect();

                if (handlers.Any())
                {
                    var message = new StringBuilder();
                    var inspector = new DependencyInspector(message);

                    foreach (IExposeDependencyInfo handler in handlers)
                    {
                        handler.ObtainDependencyDetails(inspector);
                    }

                  
                    Console.WriteLine(message.ToString());
                }


            }


        }

    }
