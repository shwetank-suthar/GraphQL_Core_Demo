import { gql } from '@apollo/client';

export const GET_USER_LOGINS = gql`
  query {
    userLogins {
      userId
      username
      email
      phoneNumber
      name
    }
  }
`;

// export const GET_USERS_BY_USERNAME = gql`
//   query GetUsersByUsername($username: String!) {
//     userLoginByUsername(username: $username) {
//       userId
//       username
//       email
//       phoneNumber
//       name
//     }
//   }
// `;

export const SEARCH_USER_LOGINS = gql`
  query UserLoginByUsername($username: String!) {
    userLoginByUsername(username: $username) {
      userId
      username
      email
      phoneNumber
      name
    }
  }
`;
