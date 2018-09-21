using System.Linq;
using AutoMapper;
using Vega.Contract.Models;
using Vega.Controllers.Resources;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API resource
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => 
                    new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features
                    .Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => 
                    new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features
                    .Select(vf => new KeyValuePairResource {Id = vf.Feature.Id, Name = vf.Feature.Name})))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));

            // API resource to domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                
                // bad
                // .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features
                //     .Select(id => new VehicleFeature { FeatureId = id  })));

                // different mapping. Here new VehcleFeature object cant be created. There must be additional processing after map
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {
                    // Remove unselected features - query over Vehicle and find if all features selected
                    
                    // traditional way
                    // var featuredToRemove = new List<VehicleFeature>();
                    // foreach (var f in v.Features)
                    //     if (!vr.Features.Contains(f.FeatureId))
                    //         // v.Features.Remove(f); <-- this will cause the runtime error, beacuse it's an iteration on v.Features, so elements from this collection cannot be removed.
                    //         featuredToRemove.Add(f);

                    //LINQ
                    //  var featuredToRemove = v.Features.Select(f => f.FeatureId).Where(id => vr.Features.Contains(id)); <-- this will return IEnumerable<int>
                    var featuresToRemove = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in featuresToRemove)
                        v.Features.Remove(f);
                    
                    // Add only new features
                    
                    // traditional way
                    // foreach (var id in vr.Features)
                    //     if(!v.Features.Any(f => f.FeatureId == id))
                    //         v.Features.Add(new VehicleFeature { FeatureId = id});

                    var featuresToAdd = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList(); 
                    foreach (var f in featuresToAdd)
                        v.Features.Add(f);
                });
        }
    }
}