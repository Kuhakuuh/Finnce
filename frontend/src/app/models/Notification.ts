export interface Notification {
    id: string;
    name: string;
    message: string;
}
export interface DataNotification{
    
    entities: Array<Notification>;
      
}