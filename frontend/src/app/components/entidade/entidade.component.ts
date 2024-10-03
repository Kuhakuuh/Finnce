import { Component, OnInit } from '@angular/core';
import { EntityService } from 'src/app/services/entity.service';
import { Entity } from 'src/app/models/Entitys';

@Component({
  selector: 'app-entidade',
  templateUrl: './entidade.component.html',
  styleUrls: ['./entidade.component.css']
})
export class EntidadeComponent implements OnInit {
  entities: Array<Entity> = [];
  entityName='';
  updateName='';
  editingMode=false;
  idEntity:string=''

  ngOnInit(): void {
    this.getAllEntitys();
  }

  constructor(
  private entityService: EntityService,
  ){}

  getAllEntitys() {
    this.entityService.getAllEntities().subscribe(
      (data: any) => {
        this.entities = data.result.$values ? data.result.$values : [data]; 
      },
      (error) => {
        console.error('Erro ao obter entities:', error);
      }
    );
  }

  onSubmit(){
    const newEntityData={
      "Name":this.entityName,
      "idUser":""
    };
    this.entityService.createEntity(newEntityData).subscribe(response => {
      window.location.reload();
    },
    error => {
      console.error('Erro ao criar entidade', error);
    }
)}

removeEntity(entityID: any) {
 
  const id:string=entityID
  this.entityService.deleteEntity(id).subscribe(result => {
    window.location.reload();
  },
  error => {
    console.error('Erro ao eliminar entidade', error);
  })
}

updateEntity() {
  if(this.updateName!=''){
  const editdEntityData={
    "Name":this.updateName,
    "idUser":"",
    "id":this.idEntity,
  };
  this.entityService.updateEntity(editdEntityData).subscribe(response => {
    window.location.reload();
  },
  error => {
    console.error('Erro ao criar entidade', error);
  }
  )
  window.location.reload();
  }else{
    this.canceledit()
  }
  
  
}

edit(entityID:any,entityName:any){
  this.editingMode=true
  this.idEntity=entityID
  this.updateName=entityName
}

canceledit(){
  this.editingMode=false
}

}

