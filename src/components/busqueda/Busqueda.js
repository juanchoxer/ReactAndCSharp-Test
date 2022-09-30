import React, { Component } from 'react';

import './Busqueda.css';

import Tarjeta from './../common/tarjeta/Tarjeta';
import Button from 'material-ui/Button';


class Busqueda extends Component {

  constructor(props){
    super(props);
    this.handleChange = this.handleChange.bind(this);
  }

  state = {
    productos: [],
    result: [],
    maxPrice: ''
  }
  
  

  componentDidMount() {
      fetch('http://localhost:62864/api/products')
       .then((res) => res.json())
       .then(productos => {
        this.setState({ productos: productos })
       })
  }
  
  

  toggleButtonState = () => {

    fetch('http://localhost:62864/api/products/precio/' + this.state.maxPrice)
       .then((res) => res.json())
       .then(resultado => {
        this.setState({ productos: resultado })
       })

  };

   handleChange(event) {
    this.setState({maxPrice: event.target.value});
  }
  

  render() {
    return (
    <div>

      <div>
        <p>Ingrese precio máximo </p>
        <input type="number" value={this.state.maxPrice} onChange={this.handleChange} ></input>
      </div>
      <br/>
      
      <Button variant="raised" color="primary" onClick={this.toggleButtonState}>Filtrar</Button>

      <div className="contenedor">
        <div className="lista-tarjetas">
          {
            this.state.productos.map((producto, index) => {
              return <Tarjeta
                key={index}
                id={producto.id}
                precio={producto.precio}
                foto={require('./../../assets/prodCategoria' + (producto.categoria +1) + '.png')} />
            })
          }
        </div>
      </div >
    </div>

    );
  }
  
  
 
}

export default Busqueda;
