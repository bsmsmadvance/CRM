using AutoFixture;
using Base.DTOs.SAL;
using Database.Models.MasterKeys;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sale.UnitTests
{
    public class ChangePromotionWorkflowServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void CreateChangePromotionWorkflow()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var userId = await db.Users.Select(o => o.ID).FirstAsync();
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        ChangePromotionWorkflowDTO changePromotionWorkflow = new ChangePromotionWorkflowDTO()
                        {
                            ApproveDate = DateTime.Now
                        };

                        changePromotionWorkflow.RequestByUser = new Base.DTOs.USR.UserListDTO()
                        { 
                            Id = userId
                        };
                        changePromotionWorkflow.PromotionType = new Base.DTOs.MST.MasterCenterDropdownDTO()
                        {
                            Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionType && o.Key == PromotionTypeKeys.Booking).Select(o => o.ID).FirstAsync()
                        };

                        changePromotionWorkflow.BookingPromotion = new UnitInfoBookingPromotionDTO();


                        // Act
                        var service = new ChangePromotionWorkflowService(db);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
