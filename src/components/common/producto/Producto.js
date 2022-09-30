import React, { Component } from 'react';

import './Producto.css';

class Producto extends Component {

  render() {
    return (
      <div>
        <img className={"product-foto"} src={this.props.foto} alt={this.props.id} />
      </div>
    );
  }
}

export default Producto;
