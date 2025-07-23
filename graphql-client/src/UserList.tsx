import React, { useState } from 'react';
import { useQuery, useMutation } from '@apollo/client';
import { GET_USER_LOGINS } from './graphql/queries';
import { UPDATE_USER_LOGIN } from './graphql/mutations';

export default function UserList() {
  const { loading, error, data, refetch } = useQuery(GET_USER_LOGINS, { variables: { limit: 100 } });
  const [updateUserLogin] = useMutation(UPDATE_USER_LOGIN);

  const [editUserId, setEditUserId] = useState<number | null>(null);
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    phoneNumber: '',
    name: ''
  });

  if (loading) return <p>Loading users...</p>;
  if (error) return <p>Error loading users: {error.message}</p>;

  const startEdit = (user: any) => {
    setEditUserId(user.userId);
    setFormData({
      username: user.username || '',
      email: user.email || '',
      phoneNumber: user.phoneNumber || '',
      name: user.name || ''
    });
  };

  const handleSave = async () => {
    try {
      await updateUserLogin({
        variables: {
          userId: editUserId,
          ...formData,
        },
      });
      setEditUserId(null);
      refetch();
    } catch (err: any) {
      console.error("Update failed:", err.message, err.graphQLErrors, err.networkError);
      alert("Update failed. Check console for details.");
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  return (
    <div style={{ padding: 20, fontFamily: 'Arial' }}>
      <h2 style={{ textAlign: 'center' }}>All Users</h2>
      <table style={{ borderCollapse: 'collapse', width: '100%' }}>
        <thead>
          <tr style={{ backgroundColor: '#f0f0f0' }}>
            <th>User ID</th>
            <th>Username</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.userLogins.map((user: any) => (
            <tr key={user.userId}>
              <td>{user.userId}</td>
              <td>
                {editUserId === user.userId ? (
                  <input name="username" value={formData.username} onChange={handleChange} />
                ) : (
                  user.username
                )}
              </td>
              <td>
                {editUserId === user.userId ? (
                  <input name="email" value={formData.email} onChange={handleChange} />
                ) : (
                  user.email
                )}
              </td>
              <td>
                {editUserId === user.userId ? (
                  <input name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} />
                ) : (
                  user.phoneNumber
                )}
              </td>
              <td>
                {editUserId === user.userId ? (
                  <input name="name" value={formData.name} onChange={handleChange} />
                ) : (
                  user.name
                )}
              </td>
              <td>
                {editUserId === user.userId ? (
                  <button onClick={handleSave}>Save</button>
                ) : (
                  <button onClick={() => startEdit(user)}>Edit</button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
