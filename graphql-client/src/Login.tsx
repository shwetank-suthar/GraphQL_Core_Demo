import React, { useState } from 'react';
import { gql, useMutation } from '@apollo/client';

const LOGIN_MUTATION = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      success
      message
    }
  }
`;

export default function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [login, { data, loading, error }] = useMutation(LOGIN_MUTATION);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await login({ variables: { input: { username, password } } });
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <input value={username} onChange={e => setUsername(e.target.value)} placeholder="Username" />
        <input value={password} onChange={e => setPassword(e.target.value)} placeholder="Password" type="password" />
        <button type="submit">Login</button>
      </form>
      {data && <p>{data.login.message}</p>}
      {error && <p style={{ color: 'red' }}>{error.message}</p>}
    </div>
  );
}
