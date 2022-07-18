import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeView } from '../views/home/home.view';
import { AccountComponent } from '../views/account/account.component';
import { FavoriteComponent } from '../components/favorite/favorite.component';
import { StayListView } from 'src/views/stay/list/stay-list.view';
import { EditStayView } from 'src/views/stay/edit/edit.view';
import { StayInformationView } from 'src/views/stay/information/stay-information.view';

const routes: Routes = [
    {
        path: 'home',
        component: HomeView
    },
    // {
    //     path: 'account',
    //     component: AccountComponent
    // },
    // {
    //     path: 'favorites',
    //     component: FavoriteComponent
    // },
    {
        path: 'stays-list',
        component: StayListView
    },
    {
        path: 'edit-stay',
        component: EditStayView
    },
    {
        path: 'stay/:stayId',
        component: StayInformationView
    },
    {
        path: '**',
        redirectTo: 'home',
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
