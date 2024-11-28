import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-edit-event',
  standalone: true,
  imports: [FormsModule,RouterLink],
  templateUrl: './edit-event.component.html',
  styleUrl: './edit-event.component.css'
})
export class EditEventComponent {
  events: any[] = [];
  eventTypes: any[] = [];
  event = {
    id: 0,
    customerName: "",
    noOfGuests: 0,
    eventTypeId: 0,
    date: new Date().toISOString(), // Ensure date format is compatible with your backend
  }

  eventId:any = 0;


  constructor(private http: HttpClient, private route:ActivatedRoute) {
    this.getEventTypes();
    this.getEventDetail();
  }
  getEventDetail(){
    this.eventId=this.route.snapshot.paramMap.get("id");
    console.log(this.eventId);
    this.http.get("https://localhost:7065/api/EventOrganizer"+this.eventId).subscribe((res: any) =>{
      this.event = res;
      console.log(res);
    })
  }

  getEventTypes() {
    this.http.get("https://localhost:7065/api/EventOrganizer/EventTypes").subscribe(
      (res: any) => {
        this.eventTypes = res;
        console.log(res);
      },
      (error) => {
        console.error("Error fetching event types", error);
      }
    );
  }

  getEventType() {
    this.http.get("https://localhost:7065/api/EventOrganizer/EventTypes").subscribe((res: any) =>{
      this.eventTypes = res;
      console.log(res);
    })
  }

  
  editEvent() {
    this.http.put("https://localhost:7065/api/EventOrganizer", this.event).subscribe(
      (res: any) => {
    if (res != null){
      alert("Event Updated successfully")
      location.href = "/";
    }else{
      alert("Failed to update event");
    }
      }
    );
  }
}
