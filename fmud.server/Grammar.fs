module fmud.Grammar
    open System.Data.Entity.Design.PluralizationServices
    let pluralizer = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Threading.Thread.CurrentThread.CurrentUICulture)
    let pluralise = pluralizer.Pluralize
    let singularise = pluralizer.Singularize
