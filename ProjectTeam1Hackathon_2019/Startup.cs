﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectTeam1Hackathon_2019.Startup))]
namespace ProjectTeam1Hackathon_2019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
