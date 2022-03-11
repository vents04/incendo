namespace Data.Models
{
    public abstract class IArtefactable<T>
    {
        public abstract Artefact GetArtefact();

        public static T FromArtefact(Artefact artefact)
        { return default(T); }
    }
}