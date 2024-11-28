import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'add-event',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent {
  events: any[] = [];
  eventTypes: any[] = [];
  event = {
    id: 0,
    customerName: "",
    noOfGuests: 0,
    eventTypeId: 0,
    date: new Date().toISOString(), // Ensure date format is compatible with your backend
  };

  constructor(private http: HttpClient) {
    this.getEventTypes();
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

  AddEvent() {
    this.http.post("https://localhost:7065/api/EventOrganizer", this.event).subscribe(
      (res: any) => {
        if (res != null) {
          alert("Event added Successfully");
        } else {
          alert("Denied");
        }
      },
      (error) => {
        console.error("Error adding event", error);
        alert("An error occurred while adding the event");
      }
    );
  }
}
