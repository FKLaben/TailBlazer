using System;
using System.IO;
using System.Reactive.Concurrency;
using TailBlazer.Domain.Annotations;
using TailBlazer.Domain.FileHandling;
using TailBlazer.Domain.Infrastructure;
using TailBlazer.Domain.Settings;
using TailBlazer.Infrastucture;

namespace TailBlazer.Views.Tail
{
    public class TailViewModelFactory: IViewModelFactory
    {
        private readonly IObjectProvider _objectProvider;
        private readonly ISchedulerProvider _schedulerProvider;

        public TailViewModelFactory([NotNull] IObjectProvider objectProvider, [NotNull] ISchedulerProvider schedulerProvider) 
        {
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
            _schedulerProvider = schedulerProvider ?? throw new ArgumentNullException(nameof(schedulerProvider));
        }
        
        public HeaderedView Create(ViewState state)
        {
            var converter = new TailViewToStateConverter();
            var converted = converter.Convert(state.State);

            var file = converted.FileName;
            var viewModel = CreateView(new FileInfo(file), converted.DisplayName, converted.AutoTail);

            var restorer = (IPersistentView)viewModel;
            restorer.Restore(state);
            return new HeaderedView(new FileHeader(new FileInfo(file), viewModel.DisplayName), viewModel);
        }

        public HeaderedView Create(FileInfo fileInfo, string displayName, bool autoTail)
        {
            var viewModel = CreateView(fileInfo, displayName, autoTail);
            viewModel.ApplySettings();//apply default values
            return new HeaderedView(new FileHeader(fileInfo, displayName), viewModel);
        }

        private TailViewModel CreateView(FileInfo fileInfo, string displayName, bool autoTail)
        {
            if (fileInfo == null) throw new ArgumentNullException(nameof(fileInfo));

            var fileWatcher = _objectProvider.Get<IFileWatcher>(new IArgument[]
            {
                new Argument<FileInfo>(fileInfo),
                new Argument<IScheduler>(_schedulerProvider.Background)
            });

           // var combined = _objectProvider.Get<ICombinedSearchMetadataCollection>();

            var args = new IArgument[]
            {
                new Argument<IFileWatcher>(fileWatcher),
               // new Argument<ICombinedSearchMetadataCollection>(combined)
            };

            var tailViewModel = _objectProvider.Get<TailViewModel>(args);
            tailViewModel.AutoTail = autoTail;
            tailViewModel.DisplayName = displayName;
            return tailViewModel;
        }
        
        public string Key => TailViewModelConstants.ViewKey;
    }
}