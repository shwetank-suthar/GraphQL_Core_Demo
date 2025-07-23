import React from 'react';
import Login from './Login';
import UserList from './UserList';
import './App.css';
import CreateUserForm from './CreateUserForm';

function App() {
  return (
    <div className="App">
      <h1>GraphQL User Admin</h1>
      <CreateUserForm />
      <UserList />
    </div>
  );
}

export default App;