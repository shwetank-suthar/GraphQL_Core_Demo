import React from 'react';
import { useQuery } from '@apollo/client';
import { GET_USER_LOGINS } from './graphql/queries';

export default function UserList() {
  const { loading, error, data } = useQuery(GET_USER_LOGINS, {variables: { limit: 100 } });

  if (loading) return <p>Loading users...</p>;
  if (error) return <p>Error loading users: {error.message}</p>;

  return (
    <div>
      <h2>All Users</h2>
      <table border={1} cellPadding={8}>
        <thead>
          <tr>
            <th>User ID</th>
            <th>Username</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {data.userLogins.map((user: any) => (
            <tr key={user.userId}>
              <td>{user.userId}</td>
              <td>{user.username}</td>
              <td>{user.email}</td>
              <td>{user.phoneNumber}</td>
              <td>{user.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
