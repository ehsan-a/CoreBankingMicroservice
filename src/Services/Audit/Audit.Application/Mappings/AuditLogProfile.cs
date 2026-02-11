using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Audit.Application.Mappings
{
    public class AuditLogProfile : Profile
    {
        public AuditLogProfile()
        {

            CreateMap(typeof(object), typeof(AuditLog))
                .ForMember("EntityName", opt => opt.MapFrom((src, dest) => src.GetType().Name))
                .ForMember("EntityId", opt => opt.MapFrom((src, dest) =>
                {
                    var prop = src.GetType().GetProperty("Id");
                    return prop != null ? prop.GetValue(src) : null;
                }))
                .ForMember("NewValue", opt => opt.MapFrom(src => JsonSerializer.Serialize(src, new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                    WriteIndented = false
                })))
                .ForMember("ActionType", opt => opt.Ignore())
                .ForMember("PerformedBy", opt => opt.Ignore())
                .ForMember("OldValue", opt => opt.Ignore());
        }
    }
}
