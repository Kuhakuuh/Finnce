export interface Account {
    id?: string;
    amountUnscaledValue?: number;
    amountScale?: number;
    amount?: number;
    currencyCode?: string;
    name?: string;
    description?: string;
    typeAccount?: string;
    lastRefreshed?: Date;
    statusBlockedTransation?: boolean;
    accountId?: string;
    iban?: string;
    idUser?: string;
  }
  
  export interface Accounts {
    contas: Array<Account>;
  }