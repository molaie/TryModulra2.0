﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yooshina.Core.Domain.Models
{
    public class EntityWithTypedId<TId> : IEntityWithTypedId<TId>
    {
        public TId Id { get; protected set; }
    }
}
