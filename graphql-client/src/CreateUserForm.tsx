import React, { useState } from 'react';
import { useMutation } from '@apollo/client';
import { CREATE_USER_LOGIN } from './graphql/mutations';

export default function CreateUserForm() {
  const [input, setInput] = useState({
    username: '',
    password: '',
    email: '',
    name: '',
    phoneNumber: '',
  });

  const [createUserLogin, { data, loading, error }] = useMutation(CREATE_USER_LOGIN);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInput({ ...input, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await createUserLogin({ variables: { input } });
    setInput({ username: '', password: '', email: '', name: '', phoneNumber: '' });
  };

  return (
    <div>
      <h2>Create New User</h2>
      <form onSubmit={handleSubmit}>
        <input name="username" placeholder="Username" value={input.username} onChange={handleChange} />
        <input name="password" placeholder="Password" type="password" value={input.password} onChange={handleChange} />
        <input name="email" placeholder="Email" value={input.email} onChange={handleChange} />
        <input name="name" placeholder="Name" value={input.name} onChange={handleChange} />
        <input name="phoneNumber" placeholder="Phone" value={input.phoneNumber} onChange={handleChange} />
        <button type="submit" disabled={loading}>Create</button>
      </form>
      {data && <p>User created: {data.createUserLogin.username}</p>}
      {error && <p style={{ color: 'red' }}>{error.message}</p>}
    </div>
  );
}
