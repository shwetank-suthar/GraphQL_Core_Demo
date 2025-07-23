import { gql } from '@apollo/client';

export const CREATE_USER_LOGIN = gql`
  mutation CreateUserLogin($input: CreateUserLoginInput!) {
    createUserLogin(input: $input) {
      userId
      username
      email
    }
  }
`;