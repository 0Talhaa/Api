import { Routes } from '@angular/router';
import { EventsComponent } from './events/events.component';
import { AddEventComponent } from './add-event/add-event.component';

export const routes: Routes = [

    {
        path:"",
        component:EventsComponent
        
    },
    {
        path:"addEvent",
        component:AddEventComponent
        
    },
    {
        path:"editEvent/:id",
        component:AddEventComponent
        
    },
];
