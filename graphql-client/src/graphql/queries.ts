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