import { Routes } from '@angular/router';
import { ProductList } from './features/products/pages/list/list';
import { Login } from './features/auth/pages/login/login';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: '', component: ProductList, children : [
    {

    }
  ] },
  { path: 'login', component: Login }
];
