export interface Transaction{
    id?: string;
    amount: number;
    type:string;
    currencyCode?: string;
    descriptionDisplay?: string;
    dateBokeed: string;
    providerTransactionId?: string;
    idAccount?: string;
    idUser?:string;
    idCategory: string;
}

export interface DataTransaction{
    
    result: Transaction[];
      
}