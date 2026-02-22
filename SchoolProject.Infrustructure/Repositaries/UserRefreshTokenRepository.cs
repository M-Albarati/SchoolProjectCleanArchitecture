using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Bases;
using SchoolProject.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositaries
{
    public class UserRefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken> ,
                                              IUserRefreshTokenRepository
                                            
    {
        #region Fields
        private DbSet<UserRefreshToken> _userRefreshToken;
        #endregion

        #region Constructor
        public UserRefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
            _userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
