﻿using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Cooking_School_ASP.NET.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //private IGenericRepository<Trainee> _trainees;
        //private IGenericRepository<Chef> _chefs;
        private IGenericRepository<User> _users;
        private IGenericRepository<Course> _courses;
        private IGenericRepository<CookClass> _cookClasses;
        private IGenericRepository<ClassDays> _classDays;
        private IGenericRepository<Project> _projects;
        private IGenericRepository<ProjectFile> _projectFiles;
        private IGenericRepository<ApplicationT> _applications;
        private IGenericRepository<FavoriteMeal_chef> _favoriteMeal_Chefs;
        private IGenericRepository<FavoriteMeal_Trainee> _favoriteMeal_Trainees;
        private IGenericRepository<Favorite_Chef> _favorite_Chefs;
        private IGenericRepository<Favorite_Course> _favorite_Courses;
        private IGenericRepository<Trainee_Course> _trainee_Course;
        private readonly DBContext _dBContext;
        public UnitOfWork(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_dBContext);
        public IGenericRepository<Course> Courses => _courses ??= new GenericRepository<Course>(_dBContext);
        public IGenericRepository<CookClass> CookClasses => _cookClasses ??= new GenericRepository<CookClass>(_dBContext);
        public IGenericRepository<ClassDays> ClassDays => _classDays ??= new GenericRepository<ClassDays>(_dBContext);
        public IGenericRepository<Project> Projects => _projects ??= new GenericRepository<Project>(_dBContext);
        public IGenericRepository<Trainee_Course> Trainee_Courses => _trainee_Course ??= new GenericRepository<Trainee_Course>(_dBContext);
        public IGenericRepository<ProjectFile> ProjectFiles => _projectFiles ??= new GenericRepository<ProjectFile>(_dBContext);
        public IGenericRepository<ApplicationT> Applications => _applications ??= new GenericRepository<ApplicationT>(_dBContext);
        public IGenericRepository<FavoriteMeal_chef> FavoriteMeal_Chefs => _favoriteMeal_Chefs ??= new GenericRepository<FavoriteMeal_chef>(_dBContext);
        public IGenericRepository<FavoriteMeal_Trainee> FavoriteMeal_Trainees => _favoriteMeal_Trainees ??= new GenericRepository<FavoriteMeal_Trainee>(_dBContext);
        public IGenericRepository<Favorite_Chef> Favorite_Chefs => _favorite_Chefs ??= new GenericRepository<Favorite_Chef>(_dBContext);
        public IGenericRepository<Favorite_Course> Favorite_Courses => _favorite_Courses ??= new GenericRepository<Favorite_Course>(_dBContext);
        public void Dispose()
        {
            _dBContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dBContext.SaveChangesAsync();
        }
    }
}
