using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Identity
{
    public class ApplicationRole:IdentityRole
    {
       
        public string Description { get; set; } //admin rolüne kimler sahip olabilir yada kurumsal rölüne gibi
        public ApplicationRole()
        {
        }
        public ApplicationRole(string rolename, string description)
        { 
        
            this.Description = description;
        }
    
    
    }
}