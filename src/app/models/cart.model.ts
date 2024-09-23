export interface Cart {
    cartID: number;
    cartItems: CartItem[];
    userID : number;
    totalprice : number;
    // Add other cart properties here
  }
  
  export interface CartItem {
    cartItemId : number;
    productId: number;
    CartId: number;
    quantity: number;
    price: number;
    name: string;
    imageURL: string;
    
    // Add other item properties here
  }

  export interface AddItemToCartRequest {
    quantity: number;
    price: number;
    imageURL: string;
    name: string;
  }
  