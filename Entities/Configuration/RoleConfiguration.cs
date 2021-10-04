using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
            public class RoleConfiguration : IEntityTypeConfiguration<Role>
            {
                        public void Configure(EntityTypeBuilder<Role> builder)
                        {
                                   builder.HasData(
                                        new Role{
                                            Name="Manger",
                                            NormalizedName="MANGER",
                                            ImportantDegree=1

                                        },
                                        new Role{
                                            Name="Adminstrator",
                                            NormalizedName="ADMINSTRATOR",
                                            ImportantDegree=2
                                        }
                                        
                                 );
                        }
            } 
}