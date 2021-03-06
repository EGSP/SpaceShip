﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game.Entities
{
    public class StarSystemMono : EntityMono
    {
        private StarSystem _starSystem;

        public List<SystemEntityMono> MonoEntities { get; set; } = new List<SystemEntityMono>();

        public TreeNode<SystemEntityMono> EntityTree;

        public void Accept(StarSystem starSystem)
        {
            _starSystem = starSystem;
        }

        private void Update()
        {
            
        }
    }
}