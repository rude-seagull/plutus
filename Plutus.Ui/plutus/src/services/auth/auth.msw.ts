/**
 * Generated by orval v6.5.1 🍺
 * Do not edit manually.
 * Plutus.Api
 * OpenAPI spec version: v1
 */
import {
  rest
} from 'msw'
import faker from 'faker'

export const getAuthenticateMock = () => ({userName: faker.helpers.randomize([faker.random.word(), null]), email: faker.helpers.randomize([(() => faker.internet.email())(), null]), roles: [...Array(faker.datatype.number({min: 1, max: 10}))].map(() => (faker.random.word())), token: faker.helpers.randomize([faker.random.word(), null])})

export const getAuthMSW = () => [
rest.post('*/api/users/authenticate', (req, res, ctx) => {
        return res(
          ctx.delay(1000),
          ctx.status(200, 'Mocked status'),
ctx.json(getAuthenticateMock()),
        )
      }),]
