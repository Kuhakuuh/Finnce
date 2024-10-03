export interface Category{
    id?: string;
    name?: string;
    IdUser?: string;
    IdCategoria?: string;
    searchTerms:string;
}

export interface DataCategory{
    
    categorias: Array<Category>;
      
}