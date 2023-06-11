import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';

export default observer(function ServerError() {
  const { commonStore } = useStore();

  return (
    <div className="flex items-center justify-center">
      <div
        className="inline-block max-w-[90vw]  bg-[conic-gradient(at_top_right,_var(--tw-gradient-stops))] from-red-400 via-red-600 to-red-200  p-6 text-white 
			drop-shadow-lg bg-opacity-80 shadow-lg rounded-md border-blue-700  my-4 capitalize"
      >
        <div className="text-center inline">
          <h2 className="text-2xl">
            <span className="font-bold">Server Error </span>
            <span className="text-xl italic">
              {commonStore.error?.message}
            </span>{' '}
          </h2>
        </div>
        {commonStore.error?.details && (
          <div className="overflow-clip break-words">
            <h4 className="mt-8">Stack trace</h4>
            <code className="mt-8 text-[0.6rem] ">
              {commonStore.error.details}
            </code>
          </div>
        )}
      </div>
    </div>
  );
});
