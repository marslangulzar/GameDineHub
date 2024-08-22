using DataModel.GenericRepository;
using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace DataModel.UnitOfWork
{
    public class UnitOfWork
    {
        #region Constructor
        public UnitOfWork()
        {
            _context = new FireStreetPizzaEntities();
        }
        #endregion

        #region Private member variables...

        private FireStreetPizzaEntities _context = null;
        private GenericRepository<UserInfo> _userRepository;
        private GenericRepository<UserRole> _userToRoleRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<AppSetting> _appSettingRepository;
        private GenericRepository<SiteProprity> _siteProprityRepository;
        private GenericRepository<VotingRound> _votingRoundRepository;
        private GenericRepository<Team> _teamRepository;
        private GenericRepository<TeamSong> _teamSongRepository;
        private GenericRepository<VotingResult> _votingResultRepository;

        #endregion

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<UserInfo> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<UserInfo>(_context);
                return _userRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for UserToRole repository.
        /// </summary>
        public GenericRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (this._userToRoleRepository == null)
                    this._userToRoleRepository = new GenericRepository<UserRole>(_context);
                return _userToRoleRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for Role repository.
        /// </summary>
        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                    this._roleRepository = new GenericRepository<Role>(_context);
                return _roleRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for votingRoundRepository repository.
        /// </summary>
        public GenericRepository<VotingRound> VotingRoundRepository
        {
            get
            {
                if (this._votingRoundRepository == null)
                    this._votingRoundRepository = new GenericRepository<VotingRound>(_context);
                return _votingRoundRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for Team repository.
        /// </summary>
        public GenericRepository<Team> TeamRepository
        {
            get
            {
                if (this._teamRepository == null)
                    this._teamRepository = new GenericRepository<Team>(_context);
                return _teamRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for TeamSong repository.
        /// </summary>
        public GenericRepository<TeamSong> TeamSongRepository
        {
            get
            {
                if (this._teamSongRepository == null)
                    this._teamSongRepository = new GenericRepository<TeamSong>(_context);
                return _teamSongRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for VotingResult repository.
        /// </summary>
        public GenericRepository<VotingResult> VotingResultRepository
        {
            get
            {
                if (this._votingResultRepository == null)
                    this._votingResultRepository = new GenericRepository<VotingResult>(_context);
                return _votingResultRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for Role To Action repository.
        /// </summary>
        public GenericRepository<AppSetting> AppSettingRepository
        {
            get
            {
                if (this._appSettingRepository == null)
                    this._appSettingRepository = new GenericRepository<AppSetting>(_context);
                return _appSettingRepository;
            }
        }

        public GenericRepository<SiteProprity> SiteProprityRepository
        {
            get
            {
                if (this._siteProprityRepository == null)
                    this._siteProprityRepository = new GenericRepository<SiteProprity>(_context);
                return _siteProprityRepository;
            }
        }


        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
