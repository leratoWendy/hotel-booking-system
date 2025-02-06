import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './Components/admin-home/admin-home.component';
import { AdminGuestsListComponent } from './Components/Guests/admin-guests-list/admin-guests-list.component';

const routes: Routes = [
  {
    path: '',
    component: AdminHomeComponent,
    children: [
    

    { path: 'gstlist', component: AdminGuestsListComponent},
    //   { path: 'brks/create', component: AdmBrokerAddNewComponent },
    //   { path: `brks/:id`, component: AdmBrokerDetailsComponent },
    //   { path: `brks/update/:id`, component: AdmBrokerUpdateComponent },
    //   { path: `brks/delete/:id`, component: AdmBrokerDeleteComponent },

    //   { path: 'stkex', component: AdmStockexListComponent },
    //   { path: `stkex/:id`, component: StockexDetailsComponent },
    //   { path: 'clients', component: AdmCompanyListComponent },
    //   { path: `clients/:id`, component: AdmCompanyDetailsComponent },

    //   { path: 'places', component: AdmPlacesListComponent },
    //   { path: 'places/create', component: AdmPlaceAddNewComponent },
    //   { path: `places/:id`, component: AdmPlaceDetailsComponent },
    //   { path: `places/update/:id`, component: AdmPlaceUpdateComponent },
    //   { path: `places/delete/:id`, component: AdmPlaceDeleteComponent },

    //   { path: 'currency', component: AdmCurrencyListComponent },
    //   { path: 'currency/create', component: AdmCurrencyAddNewComponent },
    //   { path: `currency/:id`, component: AdmCurrencyDetailsComponent },
    //   { path: `currency/update/:id`, component: AdmCurrencyUpdateComponent },
    //   { path: `currency/delete/:id`, component: AdmCurrencyDeleteComponent }
    ],
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
