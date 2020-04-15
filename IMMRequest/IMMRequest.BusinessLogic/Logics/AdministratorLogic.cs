using System;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using IMMRequest.DataAccess.Interface;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic : ILogic<Administrator>
    {
        public IRepository<Administrator> administratorRepository;

		public AdministratorLogic(IRepository<Administrator> adminRepository) 
        {
            this.administratorRepository = adminRepository;
		}

        public void Add(Administrator administrator)
        {
            this.administratorRepository.Add(administrator);
        }

        public void Save()
        {
            this.administratorRepository.Save();
        }        

        public Guid Create(Administrator administrator) 
        {
            try
            {
                this.Add(administrator);
                this.administratorRepository.Save();
                return administrator.Id;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

        public void Remove(Guid id) 
        {
            try 
            {
                Administrator administrator = this.administratorRepository.Get(id);
                this.administratorRepository.Remove(administrator);
                this.administratorRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public Administrator Update(Guid id, Administrator administrator) 
        {
            try
            {
                Administrator administratorToUpdate = this.administratorRepository.Get(id);
                administratorToUpdate.Email = administrator.Email;
                administratorToUpdate.Name = administrator.Name;
                administratorToUpdate.Password = administrator.Password;
                this.administratorRepository.Update(administratorToUpdate);
                this.administratorRepository.Save();
                return administratorToUpdate;
            }
            catch
            {
                throw new ArgumentException("Invalid guid");
            }
        }

		public Administrator Get(Guid id) 
        {
            try
            {
                return this.administratorRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<Administrator> GetAll() 
        {
            IEnumerable<Administrator> administrators = this.administratorRepository.GetAll();
            
            if (administrators.Count() == 0) 
            {
                throw new ArgumentException("There are no Administrators");
            }

            return administrators;
		}

        public void Update(Administrator entity)
        {
            throw new NotImplementedException();
        }
    }
}