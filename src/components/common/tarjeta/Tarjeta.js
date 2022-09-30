import React, { Component } from 'react';

import Card, { CardHeader, CardContent, } from 'material-ui/Card';
import Typography from 'material-ui/Typography';

import './Tarjeta.css';

import Producto from './../producto/Producto';

class Tarjeta extends Component {
    render() {
        return (
            <div className="tarjetas">
                <Card className="card-product">
                    <CardHeader title="" align="center" variant="heading" />
                    <div className="foto-price">
                        <Producto id={this.props.id} foto={this.props.foto} />
                        <Typography color="primary" className="price-game">
                            ${this.props.precio}
                        </Typography>
                    </div>
                    <CardContent>
                        <Typography variant="title" component="h2" align="center" color="primary">
                            Producto {this.props.id}
                        </Typography>
                    </CardContent>
                </Card>
            </div>
        );
    }
}

export default Tarjeta;
