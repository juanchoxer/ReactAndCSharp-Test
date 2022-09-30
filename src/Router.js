import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import Busqueda from './components/busqueda/Busqueda'
import NotFound from './components/common/not-found/NotFound';

const Router = () => (
    <BrowserRouter>
        <Switch>
            <Route exact path='/' component={Busqueda} />
            <Route path='/busqueda' component={Busqueda} />
            <Route component={NotFound} />
        </Switch>
    </BrowserRouter>
)

export default Router;