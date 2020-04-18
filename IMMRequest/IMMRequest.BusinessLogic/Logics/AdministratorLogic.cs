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
            try
            {
                this.administratorRepository.Add(administrator);
            }
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public void Save()
        {
            this.administratorRepository.Save();
        }        

        public Guid Create(Administrator administrator) 
        {   
            try {
                this.administratorRepository.Add(administrator);
                this.administratorRepository.Save();
                return administrator.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
            
        }

        public void Remove(Administrator administrator) 
        {
            try
            {
                this.administratorRepository.Remove(administrator);
                this.administratorRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid guid");
            }
        }

        public void Update(Administrator administrator) 
        {
            try
            {
                Administrator administratorToUpdate = this.administratorRepository.Get(administrator.Id);
                administratorToUpdate.Email = administrator.Email;
                administratorToUpdate.Name = administrator.Name;
                administratorToUpdate.Password = administrator.Password;
                this.administratorRepository.Update(administratorToUpdate);
                this.administratorRepository.Save();
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
    }
}