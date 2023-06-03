import { Form, FormGroup, Label, Input } from 'reactstrap';

export default function LoginForm() {
  return (
    <Form>
      <FormGroup>
        <Label for="exampleEmail">Email</Label>
        <Input id="exampleEmail" name="email" type="email" />
      </FormGroup>
      <FormGroup>
        <Label for="examplePassword">Password</Label>
        <Input id="examplePassword" name="password" type="password" />
      </FormGroup>
    </Form>
  );
}
