using System.Collections.Generic;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IRepository;
using Alfursan.IService;

namespace Alfursan.Service
{
    public class AlfursanFileService : IAlfursanFileService
    {
        private IAlfursanFileRepository fileRepository;

        public AlfursanFileService()
        {
            fileRepository = IocContainer.Resolve<IAlfursanFileRepository>();
        }

        public Responder Set(AlfursanFile file)
        {
            return file.FileId == 0
                ? fileRepository.Set(file)
                : fileRepository.Update(file);
        }

        public EntityResponder<List<AlfursanFile>> GetFiles(int userId, int customerUserId)
        {
            return fileRepository.GetFiles(userId, customerUserId);
        }

        public Responder Delete(int id)
        {
            return fileRepository.Delete(id);
        }
    }
}
