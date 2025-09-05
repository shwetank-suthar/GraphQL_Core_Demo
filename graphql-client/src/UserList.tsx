import React, { useState } from 'react';
import { useQuery, useMutation } from '@apollo/client';
import { GET_USER_LOGINS, SEARCH_USER_LOGINS } from './graphql/queries';
import { UPDATE_USER_LOGIN } from './graphql/mutations';

export default function UserList() {
  const [searchTerm, setSearchTerm] = useState('');
  const [isSearching, setIsSearching] = useState(false);
  
  const {
    loading,
    error,
    data,
    refetch
  } = useQuery(isSearching ? SEARCH_USER_LOGINS : GET_USER_LOGINS, {
    variables: isSearching ? { username: searchTerm } : {},
    skip: false,
  });

  const [updateUserLogin] = useMutation(UPDATE_USER_LOGIN);

  const [editUserId, setEditUserId] = useState<number | null>(null);
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    phoneNumber: '',
    name: ''
  });

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

  // const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  //   setSearchUsername(e.target.value);
  // };

  const handleSearch = () => {
    if (searchTerm.trim()) {
      setIsSearching(true);
    } else {
      setIsSearching(false);
    }
    refetch();
  };

  const handleClearSearch = () => {
    setSearchTerm('');
    setIsSearching(false);
  };

  if (loading) return <p>Loading users...</p>;
  if (error) return <p>Error loading users: {error.message}</p>;

  return (
    <div style={{ padding: 20, fontFamily: 'Arial' }}>
      <h2 style={{ textAlign: 'center' }}>
        {isSearching ? `Search Results for "${searchTerm}"` : 'All Users'}
      </h2>

      <div style={{ marginBottom: 16 }}>
        <input
          type="text"
          placeholder="Search by username"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          onKeyPress={(e) => e.key === 'Enter' && handleSearch()}
          style={{ marginBottom: '10px', padding: '5px', width: '300px', marginRight: '10px' }}
        />
        <button onClick={handleSearch} style={{ marginRight: '10px' }}>Search</button>
        {isSearching && (
          <button onClick={handleClearSearch}>Clear</button>
        )}
      </div>

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
          {data?.userLogins?.length > 0 ? (
            data.userLogins.map((user: any) => (
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
            ))
          ) : (
            <tr>
              <td colSpan={6} style={{ textAlign: 'center' }}>
                No users found.
              </td>
            </tr>
          )}
        </tbody>

      </table>
    </div>
  );
}
