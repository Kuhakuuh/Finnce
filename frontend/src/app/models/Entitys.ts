export interface Entity{
    id?: string;
    name?: string;
    idUser?: string;
}

export interface DataEntity{
    
    entities: Array<Entity>;
      
}