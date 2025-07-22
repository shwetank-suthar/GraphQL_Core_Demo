import { ApolloClient, InMemoryCache } from '@apollo/client';

const client = new ApolloClient({
  uri: 'http://localhost:5262/graphql', // replace with your backend URL
  cache: new InMemoryCache(),
});

export default client;