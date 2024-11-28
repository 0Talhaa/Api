import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './events.component.html',
  styleUrl: './events.component.css'
})
export class EventsComponent {
events:any=[];
constructor(private http:HttpClient){
this.getEvents();

}

getEvents(){
  this.http.get("https://localhost:7065/api/EventOrganizer").subscribe((res:any)=>{
this.events= res;
console.log(res);

  })
}



}
