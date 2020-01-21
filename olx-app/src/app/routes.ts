import {Routes} from '@angular/router' ;
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component' ;
import { MessagesComponent } from './messages/messages.component' ;
import { PostComponent } from './post/post.component' ;


export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'members', component: ListComponent },
    { path: 'messages', component: MessagesComponent },
    { path: 'post', component: PostComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full'}
]