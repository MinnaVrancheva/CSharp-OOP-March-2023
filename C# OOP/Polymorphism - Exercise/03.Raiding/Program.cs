using Raiding.Factory;
using Raiding.Core;
using Raiding.Core.Interfaces;
using Raiding.Factory.Interfaces;

IHeroFactory heroFactory = new HeroFactory();
IEngine engine = new Engine(heroFactory);
engine.Run();
