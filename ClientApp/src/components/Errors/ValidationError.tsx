import { Alert } from 'reactstrap';

interface Props {
  errors: any;
}

export default function ValidationError({ errors }: Props) {
  return (
    <>
      {errors && (
        <Alert className="mt-2 p-2" color="danger">
          {errors.map((err: string, i: any) => (
            <div className="mt-1" key={i}>
              {err}{' '}
            </div>
          ))}
        </Alert>
      )}
    </>
  );
}
