﻿using SequentialGuid;

namespace eMedSchedule.Domain.Common
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = SequentialGuidGenerator.Instance.NewGuid();
        }
    }
}