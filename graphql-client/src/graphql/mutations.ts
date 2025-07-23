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

export const UPDATE_USER_LOGIN = gql`
  mutation UpdateUserLogin(
    $userId: Int!
    $username: String
    $email: String
    $phoneNumber: String
    $name: String
  ) {
    updateUserLogin(
      userId: $userId
      username: $username
      email: $email
      phoneNumber: $phoneNumber
      name: $name
    ) {
      userId
      username
      email
      phoneNumber
      name
    }
  }
`;